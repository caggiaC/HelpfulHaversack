﻿namespace Services.TreasuryAPI.Models
{
    public class Treasury
    {
        private readonly Guid _id;

        public Guid Id { get { return _id; } }

        public string Name { get; set; } = String.Empty;

        public int PP { get; set; } = 0;
        public int GP { get; set; } = 0;
        public int EP { get; set; } = 0;
        public int SP { get; set; } = 0;
        public int CP { get; set; } = 0;

        public List<Item> Inventory { get; set; } = new();

        public Treasury()
        {
            _id = Guid.NewGuid();
        }

        public Treasury(Guid id)
        {
            _id = id;
        }
    }
}
