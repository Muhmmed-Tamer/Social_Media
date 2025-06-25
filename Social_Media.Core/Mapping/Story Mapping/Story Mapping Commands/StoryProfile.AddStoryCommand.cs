using Social_Media.Core.Features.Story.Commands.Models;
using Social_Media.Data.Models.Story;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Mapping.Story_Mapping
{
    public partial class StoryProfile
    {
        public void MappingStoryCommand()
        {
            CreateMap<AddStoryCommand, Story>()
              .ForMember(g => g.Content, Opt => Opt.MapFrom(S => S.Content))
              .ForMember(d => d.Privacy, Opt => Opt.MapFrom(S => S.Privacy))
              .ForMember(d => d.UserId, Opt => Opt.MapFrom(S => S.UserId))
              .ReverseMap();
        }
    }
}
