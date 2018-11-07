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
    public interface ICourseFeedbackService
    {

        ApiResponseViewModel GetAll();

        ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

        ApiResponseViewModel GetById(int id);

        ApiResponseViewModel Add(CourseFeedback CourseFeedback);

        ApiResponseViewModel Update(CourseFeedback CourseFeedback);

        ApiResponseViewModel Delete(int id);

    }

    public class CourseFeedbackService : ICourseFeedbackService
    {
        private ICourseFeedbackRepository _CourseFeedbackRepository;

        private IUnitOfWork _UnitOfWork;

        public CourseFeedbackService(ICourseFeedbackRepository CourseFeedbackRepository, IUnitOfWork UnitOfWork)
        {
            this._CourseFeedbackRepository = CourseFeedbackRepository;
            this._UnitOfWork = UnitOfWork;
        }

        /// <summary>
        /// Get all CourseFeedback
        /// </summary>
        /// <returns></returns>
        public ApiResponseViewModel GetAll()
        {
            var result = new List<CourseFeedback>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _CourseFeedbackRepository.GetAll().Where(m => m.IsDeleted != true).ToList();
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
        /// Get all CourseFeedback with pagging
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public ApiResponseViewModel GetAllWithPagging(int? page, int pageSize)
        {
            var result = new List<CourseFeedback>();
            var paginationSet = new PaginationSet<CourseFeedback>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var currentPage = page ?? 1;
                int totalRow = 0;
                var query = _CourseFeedbackRepository.GetAll().Where(m => m.IsDeleted != true);
                totalRow = query.Count();

                result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
					.ToList();

                paginationSet = new PaginationSet<CourseFeedback>()
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
        /// Get CourseFeedback by id
        /// </summary>
        /// <param name="id">ID of CourseFeedback</param>
        /// <returns></returns>
        public ApiResponseViewModel GetById(int id)
        {
            var result = new CourseFeedback();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _CourseFeedbackRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    response.Result = _CourseFeedbackRepository.GetSingleById(id);
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
        /// Add new CourseFeedback
        /// </summary>
        /// <param name="obj">CourseFeedback</param>
        /// <returns></returns>
        public ApiResponseViewModel Add(CourseFeedback obj)
        {
            var result = new CourseFeedback();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _CourseFeedbackRepository.Add(obj);
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
        /// Update CourseFeedback
        /// </summary>
        /// <param name="obj">New CourseFeedback</param>
        /// <returns></returns>
        public ApiResponseViewModel Update(CourseFeedback obj)
        {
            var result = new CourseFeedback();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _CourseFeedbackRepository.CheckContains(m => m.ID == obj.ID);
                if (exists)
                {
                    _CourseFeedbackRepository.Update(obj);
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
        /// Delete CourseFeedback by Id
        /// </summary>
        /// <param name="id">ID of CourseFeedback</param>
        /// <returns></returns>
        public ApiResponseViewModel Delete(int id)
        {
            var result = new CourseFeedback();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {

                var exists = _CourseFeedbackRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _CourseFeedbackRepository.Delete(id);
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