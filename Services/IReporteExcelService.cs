using SistemaControlVentas.DTOs;

namespace SistemaControlVentas.Services;

public interface IReporteExcelService
{
    Task<byte[]> GenerarReporteDiarioAsync(DateTime fecha);
    Task<byte[]> GenerarReporteSemanalAsync(DateTime fechaInicioSemana);
    Task<byte[]> GenerarReporteMensualAsync(int año, int mes);
    Task<byte[]> GenerarReportePersonalizadoAsync(ReporteFiltroDTO filtro);
}

