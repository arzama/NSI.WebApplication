﻿using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.MeetingsRepository;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.REST.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace NSI.Tests
{
    public class MeetingsControllerTest
    {
        [Fact]
        public void Create_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<IMeetingsManipulation>();
            var controller = new MeetingsController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.Post(model: null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void Create_ReturnsNewlyCreatedMeeting()
        {
            // Arrange
            int id = 123;
            DateTime from = DateTime.Now;
            DateTime to = DateTime.Now.AddDays(10);

            var usersOnMeeting = new List<UserMeetingDto>()
                {
                    new UserMeetingDto()
                    {
                        UserId = 1
                    }
                };

            var meeting = new MeetingDto()
            {
                MeetingId = id,
                From = from,
                To = to,
                UserMeeting = usersOnMeeting
            };

            var meetingRepo = new Mock<IMeetingsRepository>();
            meetingRepo.Setup(x => x.Insert(meeting)).Returns(meeting);
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);


            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.Post(meeting);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<MeetingDto>(okResult.Value);
            Assert.Equal(id, returned.MeetingId);
            Assert.Equal(from, returned.From);
            Assert.Equal(to, returned.To);
            Assert.Equal(usersOnMeeting, returned.UserMeeting);
        }
    }
}