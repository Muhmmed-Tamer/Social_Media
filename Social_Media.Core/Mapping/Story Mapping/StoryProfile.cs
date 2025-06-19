using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Mapping.Story_Mapping
{
   public partial class StoryProfile : Profile
    {
        public StoryProfile() {
            MappingStoryCommand();
            MappingAddImageOrVideoStoryCommand();
        }

       
    }
}
