using Microsoft.AspNetCore.Mvc;
using SistemaControlVentas.DTOs;
using SistemaControlVentas.Services;

namespace SistemaControlVentas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly IReporteExcelService _reporteExcelService;

    public ReportesController(IReporteExcelService reporteExcelService)
    {
        _reporteExcelService = reporteExcelService;
    }

    [HttpGet("diario")]
    public async Task<IActionResult> GenerarReporteDiario([FromQuery] DateTime fecha)
    {
        try
        {
            var archivo = await _reporteExcelService.GenerarReporteDiarioAsync(fecha);
            var nombreArchivo = $"ReporteDiario_{fecha:yyyyMMdd}.xlsx";
            
            return File(archivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al generar el reporte: {ex.Message}");
        }
    }

    [HttpGet("semanal")]
    public async Task<IActionResult> GenerarReporteSemanal([FromQuery] DateTime fechaInicioSemana)
    {
        try
        {
            var archivo = await _reporteExcelService.GenerarReporteSemanalAsync(fechaInicioSemana);
            var fechaFin = fechaInicioSemana.AddDays(6);
            var nombreArchivo = $"ReporteSemanal_{fechaInicioSemana:yyyyMMdd}_al_{fechaFin:yyyyMMdd}.xlsx";
            
            return File(archivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al generar el reporte: {ex.Message}");
        }
    }

    [HttpGet("mensual")]
    public async Task<IActionResult> GenerarReporteMensual(
        [FromQuery] int año,
        [FromQuery] int mes)
    {
        try
        {
            if (mes < 1 || mes > 12)
            {
                return BadRequest("El mes debe estar entre 1 y 12");
            }

            var archivo = await _reporteExcelService.GenerarReporteMensualAsync(año, mes);
            var nombreArchivo = $"ReporteMensual_{año}_{mes:D2}.xlsx";
            
            return File(archivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al generar el reporte: {ex.Message}");
        }
    }

    [HttpPost("personalizado")]
    public async Task<IActionResult> GenerarReportePersonalizado([FromBody] ReporteFiltroDTO filtro)
    {
        try
        {
            var archivo = await _reporteExcelService.GenerarReportePersonalizadoAsync(filtro);
            var nombreArchivo = $"ReportePersonalizado_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            
            return File(archivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al generar el reporte: {ex.Message}");
        }
    }
}

