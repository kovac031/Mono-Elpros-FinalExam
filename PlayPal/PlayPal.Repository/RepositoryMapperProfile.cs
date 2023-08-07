using AutoMapper;
using PlayPal.DAL;
using PlayPal.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Repository
{
    public class RepositoryMapperProfile : Profile
    {
        public RepositoryMapperProfile()
        {
            CreateMap<BoardGame, BoardGameDTO>();
            CreateMap<BoardGameDTO, BoardGame>();
        }
    }
}
