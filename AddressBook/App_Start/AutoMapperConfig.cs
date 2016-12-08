using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AddressBook.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            var source = new ApplicationUser();
            AutoMapper.Mapper.Map<LoginViewModel>(source);
        }
    }
}