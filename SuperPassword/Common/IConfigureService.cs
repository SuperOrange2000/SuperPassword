using SuperPassword.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.Common
{
    internal interface IConfigureService
    {
        void Configure(UserDto user);
    }
}
