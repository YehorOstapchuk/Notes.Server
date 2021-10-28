﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    public class GetNoteDetailsQueryHandler
        :IRequestHandler<GetNoteDetailsQuery, NoteDetailsVm>
    {
        private readonly INotesDbContext dbContext;
        private readonly IMapper mapper;

        public GetNoteDetailsQueryHandler (INotesDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<NoteDetailsVm> Handle (GetNoteDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Notes
                .FirstOrDefaultAsync(note =>
                note.Id == request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            return mapper.Map<NoteDetailsVm>(entity);
        }
    }
}
