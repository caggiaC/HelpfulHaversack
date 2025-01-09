using HelpfulHaversack.Web.Models.Dto;

namespace HelpfulHaversack.Web.Models.View
{
    public class TreasuryManageViewModel
    {
        public TreasuryDto? Treasury { get; set; }

        public List<TreasuryDto>? TreasuryList { get; set; }
    }
}
