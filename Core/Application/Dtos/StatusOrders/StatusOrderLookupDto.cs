using Application.Common.Mappings;
using AutoMapper;
using Domain.Model;

namespace Application.Dtos.StatusOrders;

public class StatusOrderLookupDto : IMapWith<StatusOrder>
{
    public Guid Id { get; set; }
    public string StatusName { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StatusOrder, StatusOrderLookupDto>()
            .ForMember(statusOrderVm => statusOrderVm.Id,
                opt => opt.MapFrom(statusOrder => statusOrder.Id))
            .ForMember(statusOrderVm => statusOrderVm.StatusName,
                opt => opt.MapFrom(statusOrder => statusOrder.StatusName));
    }
}