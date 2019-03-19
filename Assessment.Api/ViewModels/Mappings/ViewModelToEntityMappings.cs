using Assessment.Api.Models.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Api.ViewModels.Mappings
{
    public class ViewModelToEntityMappings : Profile
    {
        public ViewModelToEntityMappings()
        {
            MapEntityToViewModel();
            MapViewModelToEntity();
        }

        private void MapViewModelToEntity()
        {
            CreateMap<string, DateTime>().ConvertUsing(new DateTimeStringConverter());
            CreateMap<StudentViewModel, Student>();
        }

        private void MapEntityToViewModel()
        {
            CreateMap<DateTime, string>().ConvertUsing(new StringDateTimeConverter());
            CreateMap<Student, StudentViewModel>();
        }

        internal class DateTimeStringConverter : ITypeConverter<string, DateTime>
        {
            public DateTime Convert(string source, DateTime destination, ResolutionContext context)
            {
                return System.Convert.ToDateTime(source);
            }
        }

        internal class StringDateTimeConverter : ITypeConverter<DateTime, string>
        {
            public string Convert(DateTime source,  string destination, ResolutionContext context)
            {
                return source.ToShortDateString();
            }
        }
    }
}
