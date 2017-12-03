﻿using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.MeetingsRepository;
using IkarusEntities;
using System.Linq;

namespace NSI.Repository
{
    public partial class MeetingsRepository : IMeetingsRepository
    {
        private readonly IkarusContext _dbContext;

        public MeetingsRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Insert(MeetingDto model)
        {
            try
            {
                var entity_meeting = Mappers.MeetingsRepository.MapToDbEntity(model);
                _dbContext.Meeting.Add(entity_meeting);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                // log exception
                throw new Exception("Something went wrong with database");
            }
        }
        public ICollection<MeetingDto> GetMeetings()
        {
            try
            {
                var meetings = _dbContext.Meeting;
                if (meetings != null)
                {
                    ICollection<MeetingDto> meetingDto = new List<MeetingDto>();
                    foreach (var item in meetings)
                    {
                        meetingDto.Add(Mappers.MeetingsRepository.MapToDto(item));
                    }
                    return meetingDto;
                }
            }
            catch (Exception ex)
            {
                //log ex
                throw new Exception("Database error!");
            }
            return null;
        }
        public void Update(int meetingId, MeetingDto model)
        {
            try
            {
                var meetingTmp = _dbContext.Meeting.FirstOrDefault(x => x.MeetingId == meetingId);
                if (meetingTmp != null)
                {
                    //remove all users for this meeting from UserMeeting table
                    var atendees = _dbContext.UserMeeting.Where(x => x.MeetingId == meetingId).ToList();
                    if (atendees != null)
                        _dbContext.UserMeeting.RemoveRange(atendees);

                    //update data
                    meetingTmp.DateModified = DateTime.Now;
                    meetingTmp.From = model.From != null ? model.From : meetingTmp.From;
                    meetingTmp.To = model.To != null ? model.To : meetingTmp.To;

                    //update users
                    foreach (var item in model.UserMeeting)
                        meetingTmp.UserMeeting.Add(new UserMeeting() { UserId = item.UserId, MeetingId = meetingId });

                    _dbContext.SaveChanges();

                }
            }
            catch (Exception e)
            {
                //log ex
                throw new Exception("Database error!");

            }
        }

        }
    }