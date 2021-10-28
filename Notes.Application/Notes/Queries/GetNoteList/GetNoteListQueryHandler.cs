using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQueryHandler
        : IRequestHandler<GetNoteListQuery, NoteListVm>
    {
        private readonly INotesDbContext dbContext;
        private readonly IMapper mapper;

        public GetNoteListQueryHandler(INotesDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<NoteListVm> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
        {
            var notesQuery = await dbContext.Notes.Where(note => note.UserId == request.UserId)
                                        .Where(note => note.UserId == request.UserId)
                                        .ProjectTo<NoteLookUpDto>(mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);

            return new NoteListVm { Notes = notesQuery };
        }

    }
}
