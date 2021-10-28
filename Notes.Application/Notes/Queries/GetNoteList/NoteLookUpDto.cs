using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class NoteLookUpDto : IMapWith<Note>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Note, NoteLookUpDto>()
                .ForMember(noteVm => noteVm.Title,
                    opt => opt.MapFrom(note => note.Title))
                .ForMember(noteVm => noteVm.Id,
                    opt => opt.MapFrom(note => note.Id));
        }
    }
}
