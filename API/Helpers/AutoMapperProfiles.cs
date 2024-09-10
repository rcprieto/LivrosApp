using API.Entities;
using AutoMapper;

namespace API;

public class AutoMapperProfiles : Profile
{
	public AutoMapperProfiles()
	{
		// CreateMap<DateTime, DateTime>().ConvertUsing(x => DateTime.SpecifyKind(x, DateTimeKind.Utc));
		// CreateMap<DateTime?, DateTime?>().ConvertUsing(x => x.HasValue ? DateTime.SpecifyKind(x.Value, DateTimeKind.Utc) : null);

	}

}
