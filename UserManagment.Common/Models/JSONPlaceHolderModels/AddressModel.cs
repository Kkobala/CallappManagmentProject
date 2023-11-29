﻿namespace UserManagment.Common.Models.JSONPlaceHolderModels
{
    public class AddressModel
    {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public Geo Geo { get; set; }
    }
}