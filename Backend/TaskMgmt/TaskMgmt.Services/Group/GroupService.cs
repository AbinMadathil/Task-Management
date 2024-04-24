using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.DataAccess.UnitOfWork;
using TaskMgmt.Services.CustomExceptions;
using TaskMgmt.Services.Helpers;
using TaskMgmt.Services.Interfaces;

namespace TaskMgmt.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;

        public GroupService(IGroupRepository groupRepository, INotificationService notificationService, IUserRepository userRepository,IUnitOfWork unitOfWork)
        {
            _groupRepository = groupRepository;
            _notificationService = notificationService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Group> GetById(int id)
        {
            var group =  _groupRepository.GetById(id);
            if (group == null)
            {
                throw new GroupNotFoundException("Group Not Found");
            }
            return group;
        }

        public async Task<Group[]> GetAll(int userid)
        {
            var groups =  _groupRepository.GetAll(userid);
            return groups;
        }

        public async Task<int> Add(Group group, User user)
        {
            try
            {

                bool exists = _groupRepository.CheckExists(group.GroupName);
                if (exists)
                {
                    throw new GroupAlreadyExistsException("Group already exists");
                }
                else
                {
                    _groupRepository.Add(group);
                    _groupRepository.Enroll(new UserGroup
                    {
                        Group = group,
                        User = user,
                    });

                    await _unitOfWork.CommitAsync();

                    return group.GroupId;
                }

            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        public async Task Enroll(int userId, string groupName, string referralCode)
        {
            try
            {

                User user = _userRepository.GetById(userId);

                var invitation = _groupRepository.GetInvitationByRefCode(referralCode);

                if (invitation == null)
                {
                    throw new Exception("No invitation found");
                }

                if (invitation.Group.GroupName != groupName || invitation.InviteeEmail != user.Email)
                {
                    throw new Exception("Invalid referral code");
                }

                var enrollment = new UserGroup
                {
                    GroupId = (int)invitation.GroupId,
                    UserId = userId,
                    IsAdmin = false,
                };
                _groupRepository.Enroll(enrollment);
                invitation.Status = true;
                _groupRepository.UpdateInvitation(invitation);
                await _unitOfWork.CommitAsync();

            }
            catch(Exception ex)
            {

                throw new InvalidOperationException("Error during enrollment", ex);
            }
        }
        public async Task<int> InviteUser(int userId, int groupId, string inviteeEmail)
        {
            try
            {
                Group group = _groupRepository.GetById(groupId);
                User user = _userRepository.GetById(userId);
                Invitation invitation =  _groupRepository.InviteUser(user, group, inviteeEmail);
                await _unitOfWork.CommitAsync();
                string subject = $"{user.Username} invited you to join the Group {group.GroupName}";
                string message = $"Please use this code to join the Group: {invitation.Token}";
                await _notificationService.NotifyAsync(inviteeEmail, subject, message);
                return invitation.InvitationId;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
