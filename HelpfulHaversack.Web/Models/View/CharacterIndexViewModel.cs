using HelpfulHaversack.Web.Models.Dto;

namespace HelpfulHaversack.Web.Models.View
{
    public class CharacterIndexViewModel
    {
        public CharacterDto Character { get; set; }

        public List<TreasuryReferenceDto> TreasuryList { get; set; }

        public CharacterIndexViewModel(CharacterDto characterDto, List<TreasuryReferenceDto> treasuryList)
        {
            Character = characterDto;
            TreasuryList = treasuryList;
        }
    }
}
