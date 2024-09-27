using System;
using API.DTOs;
using API.Helpers;

namespace API.Interfaces;

public interface IReportRepository
{

	Task<ReportsDto> ReportDash(FiltersParams filters);

}
