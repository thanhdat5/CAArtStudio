using System;
using System.Collections.Generic;
using System.Linq;
using CAArtStudio.Model.ResponseModel;
using CAArtStudio.Model;
using CAArtStudio.Data.Repositories;
using CAArtStudio.Data.Infrastructure;
using CAArtStudio.Common;

namespace CAArtStudio.Service
{
	public interface IUserService
	{

		ApiResponseModel GetAll();

		ApiResponseModel GetAllWithPagging(int? page, int pageSize);

		ApiResponseModel GetById(int id);
		UserResponseModel GetUserById(int id);

		ApiResponseModel Add(User User);

		ApiResponseModel Update(User User);

		ApiResponseModel Delete(int id);

	}

	public class UserService : IUserService
	{
		private IUserRepository _UserRepository;

		private IUnitOfWork _UnitOfWork;

		public UserService(IUserRepository UserRepository, IUnitOfWork UnitOfWork)
		{
			this._UserRepository = UserRepository;
			this._UnitOfWork = UnitOfWork;
		}

		/// <summary>
		/// Get all User
		/// </summary>
		/// <returns></returns>
		public ApiResponseModel GetAll()
		{
			var result = new List<User>();
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				result = _UserRepository.GetAll().Where(m => m.IsDeleted != true).ToList();
				response.Result = result;
			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}
			return response;
		}

		/// <summary>
		/// Get all User with pagging
		/// </summary>
		/// <param name="page">Current page</param>
		/// <param name="pageSize">Page size</param>
		/// <returns></returns>
		public ApiResponseModel GetAllWithPagging(int? page, int pageSize)
		{
			var result = new List<User>();
			var paginationSet = new PaginationSet<User>();
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var currentPage = page ?? 1;
				int totalRow = 0;
				var query = _UserRepository.GetAll().Where(m => m.IsDeleted != true);
				totalRow = query.Count();

				result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
					.Take(pageSize)
					.ToList();

				paginationSet = new PaginationSet<User>()
				{
					Items = result,
					Page = currentPage,
					TotalCount = result.Count(),
					TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
				};
				response.Result = paginationSet;
			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}
			return response;
		}

		/// <summary>
		/// Get User by id
		/// </summary>
		/// <param name="id">ID of User</param>
		/// <returns></returns>
		public ApiResponseModel GetById(int id)
		{
			var result = new User();
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var exists = _UserRepository.CheckContains(m => m.ID == id);
				if (exists)
				{
					response.Result = _UserRepository.GetSingleById(id);
				}
				else
				{
					response.Code = CommonConstants.ApiResponseNotFoundCode;
					response.Message = CommonConstants.NotFoundMessage;
				}
			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}

			return response;
		}

		/// <summary>
		/// Get User by id
		/// </summary>
		/// <param name="id">ID of User</param>
		/// <returns></returns>
		public UserResponseModel GetUserById(int id)
		{
			try
			{
				var exists = _UserRepository.GetSingleById(id);
				if (exists != null)
				{
					return new UserResponseModel
					{
						AboutMe = exists.AboutMe,
						Created = exists.Created,
						CreatedBy = exists.CreatedBy,
						Email = exists.Email,
						Facebook = exists.Facebook,
						FullName = exists.FullName,
						GooglePlus = exists.GooglePlus,
						GroupID = exists.GroupID,
						GroupName = exists.Group != null ? exists.Group.Name : string.Empty,
						ID = exists.ID,
						IsActive = exists.IsActive,
						IsDeleted = exists.IsDeleted,
						JobTitle = exists.JobTitle,
						LinkedIn = exists.LinkedIn,
						Modified = exists.Modified,
						ModifiedBy = exists.ModifiedBy,
						PhotoImage = exists.PhotoImage,
						Skills = exists.Skills,
						Twitter = exists.Twitter,
						UserName = exists.UserName
					};
				}
				else
				{
					return null;
				}
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// Add new User
		/// </summary>
		/// <param name="obj">User</param>
		/// <returns></returns>
		public ApiResponseModel Add(User obj)
		{
			var result = new User();
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				result = _UserRepository.Add(obj);
				_UnitOfWork.Commit();
				response.Message = CommonConstants.AddSuccess;
				response.Result = result;
			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}
			return response;

		}

		/// <summary>
		/// Update User
		/// </summary>
		/// <param name="obj">New User</param>
		/// <returns></returns>
		public ApiResponseModel Update(User obj)
		{
			var result = new User();
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var exists = _UserRepository.CheckContains(m => m.ID == obj.ID);
				if (exists)
				{
					_UserRepository.Update(obj);
					_UnitOfWork.Commit();
					response.Message = CommonConstants.SaveSuccess;
				}
				else
				{
					response.Code = CommonConstants.ApiResponseNotFoundCode;
					response.Message = CommonConstants.NotFoundMessage;
				}

			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}

			return response;
		}

		/// <summary>
		/// Delete User by Id
		/// </summary>
		/// <param name="id">ID of User</param>
		/// <returns></returns>
		public ApiResponseModel Delete(int id)
		{
			var result = new User();
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{

				var exists = _UserRepository.CheckContains(m => m.ID == id);
				if (exists)
				{
					result = _UserRepository.Delete(id);
					_UnitOfWork.Commit();
					response.Message = CommonConstants.DeleteSuccess;
					response.Result = result;
				}
				else
				{
					response.Code = CommonConstants.ApiResponseNotFoundCode;
					response.Message = CommonConstants.NotFoundMessage;
				}
			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}
			return response;
		}
	}
}