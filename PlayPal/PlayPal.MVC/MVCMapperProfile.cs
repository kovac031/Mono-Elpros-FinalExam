using AutoMapper;
using PlayPal.DAL;
using PlayPal.ModelDTO;
using PlayPal.MVC.Models;
using PlayPal.MVC.Models.BoardGameView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayPal.MVC.App_Start
{
    public class MVCMapperProfile : Profile
    {
        public MVCMapperProfile() 
        {
            CreateMap<BoardGameDTO, BoardGameListView>();
            CreateMap<BoardGameDTO, BoardGameEditView>();
            CreateMap<BoardGameEditView, BoardGameDTO>();

            CreateMap<BoardGame, BoardGameDTO>();
            CreateMap<BoardGameDTO, BoardGame>();
            //CreateMap<BoardGame, BoardGameDTO>();
        }

    }
}