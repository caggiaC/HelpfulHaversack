﻿using HelpfulHaversack.Web.Models.Dto;
using HelpfulHaversack.Web.Models.View;

namespace HelpfulHaversack.Web.Models.View
{
    public class TreasuryManageViewModel
    {
        public TreasuryDto Treasury { get; set; }

        public List<TreasuryReference> TreasuryList { get; set; }

        public TreasuryManageViewModel(TreasuryDto treasuryDto, List<TreasuryReference> treasuryList)
        {
            Treasury = treasuryDto;
            TreasuryList = treasuryList;
        }
    }
}
