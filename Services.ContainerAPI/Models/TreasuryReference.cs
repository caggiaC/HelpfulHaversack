﻿using Services.ContainerAPI.Models;
using Services.ContainerAPI.Models.Dto;

namespace HelpfulHaversack.Services.ContainerAPI.Models
{
    public class TreasuryReference
    {
        public static TreasuryReference CreateReference(TreasuryDto treasuryDto)
        {
            if (treasuryDto == null)
                throw new ArgumentNullException(nameof(treasuryDto));

            if (treasuryDto.TreasuryId != Guid.Empty && treasuryDto.Name != null)
                return new TreasuryReference(treasuryDto.Name, treasuryDto.TreasuryId);
            else
                throw new ArgumentException("TreasuryDto must have a valid TreasuryId and Name", nameof(treasuryDto));
        }

        public static TreasuryReference CreateReference(Treasury treasury)
        {
            if (treasury == null)
                throw new ArgumentNullException(nameof(treasury));

            if (treasury.TreasuryId != Guid.Empty && treasury.Name != null)
                return new TreasuryReference(treasury.Name, treasury.TreasuryId);
            else
                throw new ArgumentException("Treasury must have a valid TreasuryId and Name", nameof(treasury));
        }

        public TreasuryReference(string treasuryName, Guid treasuryId)
        {
            TreasuryName = treasuryName;
            TreasuryId = treasuryId;
        }

        public string TreasuryName { get; set; }
        public Guid TreasuryId { get; set; }
    }
}
