﻿using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.MeetingsRepository;
using IkarusEntities;
using System.Linq;
using NSI.DC.Exceptions;

namespace NSI.Repository
{
    public partial class MeetingsRepository : IMeetingsRepository
    {
        private readonly IkarusContext _dbContext;

        public MeetingsRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<MeetingDto> GetMeetings()
        {
            return _dbContext.Meeting.Where(x => x.IsDeleted == false)
                .Select(x => Mappers.MeetingsRepository.MapToDto(x)).ToList();

        }

        public MeetingDto GetMeetingById(int id)
        {
            var meeting = _dbContext.Meeting.FirstOrDefault(x => x.MeetingId == id && x.IsDeleted == false);
            if (meeting == null) throw new NSIException("Meeting not found");
            return Mappers.MeetingsRepository.MapToDto(meeting);
        }

        public void InsertMeeting(MeetingDto model)
        {
            var entity_meeting = Mappers.MeetingsRepository.MapToDbEntity(model);
            _dbContext.Meeting.Add(entity_meeting);
            _dbContext.SaveChanges();
        }

        public void UpdateMeeting(int meetingId, MeetingDto model)
        {
            var meeting = _dbContext.Meeting.FirstOrDefault(x => x.MeetingId == meetingId && x.IsDeleted == false);
            if (meeting == null) throw new NSIException("Meeting not found");
            //remove all users for this meeting from UserMeeting table
            var atendees = _dbContext.UserMeeting.Where(x => x.MeetingId == meetingId).ToList();
            if (atendees != null)
                _dbContext.UserMeeting.RemoveRange(atendees);

            //update data
            meeting.Title = model.Title ?? meeting.Title;
            meeting.DateModified = DateTime.Now;
            meeting.From = model.From != null ? model.From : meeting.From;
            meeting.To = model.To != null ? model.To : meeting.To;

            //update users
            foreach (var item in model.UserMeeting)
                meeting.UserMeeting.Add(new UserMeeting() { UserId = item.UserId, MeetingId = meetingId });

            _dbContext.SaveChanges();
        }

        public void DeleteMeeting(int meetingId)
        {
            var meeting = _dbContext.Meeting.FirstOrDefault(x => x.MeetingId == meetingId);
            if (meeting == null) throw new NSIException("Meeting not found");

            meeting.IsDeleted = true;
            meeting.DateModified = DateTime.Now;
            _dbContext.SaveChanges();
        }
    }
}
