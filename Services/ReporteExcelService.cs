using ClosedXML.Excel;
using SistemaControlVentas.DTOs;
using SistemaControlVentas.Models.Enums;

namespace SistemaControlVentas.Services;

public class ReporteExcelService : IReporteExcelService
{
    private readonly IVentaService _ventaService;

    public ReporteExcelService(IVentaService ventaService)
    {
        _ventaService = ventaService;
    }

    public async Task<byte[]> GenerarReporteDiarioAsync(DateTime fecha)
    {
        var fechaInicio = fecha.Date;
        var fechaFin = fecha.Date.AddDays(1).AddSeconds(-1);
        var ventas = await _ventaService.GetByFechaAsync(fechaInicio, fechaFin);

        return GenerarExcel(ventas, $"Reporte Diario - {fecha:dd/MM/yyyy}");
    }

    public async Task<byte[]> GenerarReporteSemanalAsync(DateTime fechaInicioSemana)
    {
        var fechaInicio = fechaInicioSemana.Date;
        var fechaFin = fechaInicio.AddDays(7).AddSeconds(-1);
        var ventas = await _ventaService.GetByFechaAsync(fechaInicio, fechaFin);

        return GenerarExcel(ventas, $"Reporte Semanal - {fechaInicio:dd/MM/yyyy} al {fechaFin:dd/MM/yyyy}");
    }

    public async Task<byte[]> GenerarReporteMensualAsync(int año, int mes)
    {
        var fechaInicio = new DateTime(año, mes, 1);
        var fechaFin = fechaInicio.AddMonths(1).AddSeconds(-1);
        var ventas = await _ventaService.GetByFechaAsync(fechaInicio, fechaFin);

        var nombreMes = fechaInicio.ToString("MMMM", new System.Globalization.CultureInfo("es-ES"));
        return GenerarExcel(ventas, $"Reporte Mensual - {nombreMes} {año}");
    }

    public async Task<byte[]> GenerarReportePersonalizadoAsync(ReporteFiltroDTO filtro)
    {
        List<VentaResponseDTO> ventas;

        if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
        {
            ventas = await _ventaService.GetByFechaAsync(filtro.FechaInicio.Value, filtro.FechaFin.Value);
        }
        else if (filtro.UsuarioId.HasValue)
        {
            ventas = await _ventaService.GetByUsuarioAsync(
                filtro.UsuarioId.Value,
                filtro.FechaInicio,
                filtro.FechaFin);
        }
        else if (filtro.Seccion.HasValue)
        {
            ventas = await _ventaService.GetBySeccionAsync(
                filtro.Seccion.Value,
                filtro.FechaInicio,
                filtro.FechaFin);
        }
        else
        {
            ventas = await _ventaService.GetAllAsync();
        }

        var titulo = "Reporte Personalizado";
        if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
        {
            titulo = $"Reporte - {filtro.FechaInicio.Value:dd/MM/yyyy} al {filtro.FechaFin.Value:dd/MM/yyyy}";
        }

        return GenerarExcel(ventas, titulo);
    }

    private byte[] GenerarExcel(List<VentaResponseDTO> ventas, string titulo)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Ventas");

        // Título
        worksheet.Cell(1, 1).Value = titulo;
        worksheet.Cell(1, 1).Style.Font.Bold = true;
        worksheet.Cell(1, 1).Style.Font.FontSize = 16;
        worksheet.Range(1, 1, 1, 8).Merge();

        // Fecha de generación
        worksheet.Cell(2, 1).Value = $"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
        worksheet.Range(2, 1, 2, 8).Merge();

        // Total de ventas
        var totalVentas = ventas.Sum(v => v.Total);
        worksheet.Cell(3, 1).Value = $"Total de Ventas: ${totalVentas:N2}";
        worksheet.Cell(3, 1).Style.Font.Bold = true;
        worksheet.Range(3, 1, 3, 8).Merge();

        // Encabezados
        var row = 5;
        worksheet.Cell(row, 1).Value = "ID";
        worksheet.Cell(row, 2).Value = "Fecha";
        worksheet.Cell(row, 3).Value = "Usuario";
        worksheet.Cell(row, 4).Value = "Sección";
        worksheet.Cell(row, 5).Value = "Productos";
        worksheet.Cell(row, 6).Value = "Cantidad Total";
        worksheet.Cell(row, 7).Value = "Total";
        worksheet.Cell(row, 8).Value = "Observaciones";

        // Estilo de encabezados
        var headerRange = worksheet.Range(row, 1, row, 8);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
        headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        row++;

        // Datos
        foreach (var venta in ventas)
        {
            var productosInfo = string.Join("; ", venta.Detalles.Select(d => $"{d.ProductoNombre} ({d.Cantidad})"));
            var cantidadTotal = venta.Detalles.Sum(d => d.Cantidad);
            var seccionNombre = ObtenerNombreSeccion(venta.Seccion);

            worksheet.Cell(row, 1).Value = venta.Id;
            worksheet.Cell(row, 2).Value = venta.FechaVenta.ToString("dd/MM/yyyy HH:mm");
            worksheet.Cell(row, 3).Value = venta.UsuarioNombre;
            worksheet.Cell(row, 4).Value = seccionNombre;
            worksheet.Cell(row, 5).Value = productosInfo;
            worksheet.Cell(row, 6).Value = cantidadTotal;
            worksheet.Cell(row, 7).Value = venta.Total;
            worksheet.Cell(row, 7).Style.NumberFormat.Format = "$#,##0.00";
            worksheet.Cell(row, 8).Value = venta.Observaciones ?? "";

            row++;
        }

        // Resumen por sección
        row += 2;
        worksheet.Cell(row, 1).Value = "RESUMEN POR SECCIÓN";
        worksheet.Cell(row, 1).Style.Font.Bold = true;
        worksheet.Range(row, 1, row, 2).Merge();
        row++;

        worksheet.Cell(row, 1).Value = "Sección";
        worksheet.Cell(row, 2).Value = "Total";
        var resumenHeaderRange = worksheet.Range(row, 1, row, 2);
        resumenHeaderRange.Style.Font.Bold = true;
        resumenHeaderRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
        row++;

        var resumenPorSeccion = ventas
            .GroupBy(v => v.Seccion)
            .Select(g => new
            {
                Seccion = ObtenerNombreSeccion(g.Key),
                Total = g.Sum(v => v.Total)
            })
            .OrderBy(r => r.Seccion);

        foreach (var resumen in resumenPorSeccion)
        {
            worksheet.Cell(row, 1).Value = resumen.Seccion;
            worksheet.Cell(row, 2).Value = resumen.Total;
            worksheet.Cell(row, 2).Style.NumberFormat.Format = "$#,##0.00";
            row++;
        }

        // Ajustar ancho de columnas
        worksheet.Column(1).Width = 8;
        worksheet.Column(2).Width = 18;
        worksheet.Column(3).Width = 20;
        worksheet.Column(4).Width = 15;
        worksheet.Column(5).Width = 40;
        worksheet.Column(6).Width = 15;
        worksheet.Column(7).Width = 15;
        worksheet.Column(8).Width = 30;

        // Aplicar bordes a los datos
        if (ventas.Any())
        {
            var dataRange = worksheet.Range(5, 1, row - resumenPorSeccion.Count() - 2, 8);
            dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    private static string ObtenerNombreSeccion(SeccionVenta seccion)
    {
        return seccion switch
        {
            SeccionVenta.Comedor => "Comedor",
            SeccionVenta.ParaLlevar => "Para Llevar",
            SeccionVenta.Autoservicio => "Autoservicio",
            SeccionVenta.ADomicilio => "A Domicilio",
            _ => seccion.ToString()
        };
    }
}

