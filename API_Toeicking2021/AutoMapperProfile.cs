using API_Toeicking2021.Dtos;
using API_Toeicking2021.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021
{
    public class AutoMapperProfile : Profile
    {
        // 要寫在建構式內
        public AutoMapperProfile()
        {
            //使用CreateMap<sourceT, destinationT>();建立物件轉換關係
            CreateMap<Sentence, SentenceDto>();
            CreateMap<Vocabulary, VocabularyDto>();
            CreateMap<GA, GADto>();
            CreateMap<VA, VADto>();
            // 將User類別中string型別的WordList轉成GetUserDto類別中List<int>型別的WordList:
            // 1. ForMember()第一個Lambda是回傳欲特別轉換的目標類別(GetUserDto)的屬性(WordList)
            // 2. u是來源類別(User)，將其屬性WordList傳入自訂方法後，回傳List<int>型別的屬性
            CreateMap<User, GetUserDto>().ForMember(dto => dto.WordList, config => config.MapFrom(u => GetWordList(u.WordList)));
            CreateMap<AddUserDto, User>();

        }
        // 把string轉成List<int>的自訂方法
        private static List<int> GetWordList(string model)
        {
            if (string.IsNullOrEmpty(model))
            {
                return null;
            }
            return model.Split(',').Select(int.Parse).ToList();
        }


    }


}
