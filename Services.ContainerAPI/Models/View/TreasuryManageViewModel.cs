using Services.ContainerAPI.Models.Dto;

namespace HelpfulHaversack.Services.ContainerAPI.Models.View
{
    public class TreasuryManageViewModel
    {
        public TreasuryDto? Treasury { get; set; }

        public List<TreasuryDto>? TreasuryList { get; set; }
    }
}
