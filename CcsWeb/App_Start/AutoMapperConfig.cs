﻿namespace CcsWeb.App_Start
{
    using AutoMapper;
    using CcsData.Models;
    using CcsData.ViewModels;
    using System;
    using System.Linq.Expressions;

    public class AutoMapperConfig
    {
        public static void MapViewModels()
        {
            Mapper.CreateMap<App1, Application>();
            Mapper.CreateMap<Application, App1>();
            Mapper.CreateMap<App2, Application>();
            Mapper.CreateMap<Application, App2>();
            Mapper.CreateMap<App3, Application>();
            Mapper.CreateMap<Application, App3>();
            Mapper.CreateMap<App4, Application>();
            Mapper.CreateMap<Application, App4>();
            Mapper.CreateMap<App5, Application>();
            Mapper.CreateMap<Application, App5>();
            Mapper.CreateMap<Mortgage, MortgageVM>();
            Mapper.CreateMap<MortgageVM, Mortgage>();
            Mapper.CreateMap<Applicant, LeadSheetVM>().ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.FirstName), opt => opt.MapFrom<string>(src => src.FirstName)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.LastName), opt => opt.MapFrom<string>(src => src.LastName));
            Mapper.CreateMap<LeadSheetVM, Applicant>().ForMember((Expression<Func<Applicant, object>>)(dest => dest.FirstName), opt => opt.MapFrom<string>(src => src.FirstName)).ForMember((Expression<Func<Applicant, object>>)(dest => dest.LastName), opt => opt.MapFrom<string>(src => src.LastName));
            Mapper.CreateMap<Address, LeadSheetVM>();
            Mapper.CreateMap<LeadSheetVM, Address>();
            Mapper.CreateMap<Credit, LeadSheetVM>();
            Mapper.CreateMap<LeadSheetVM, Credit>();
            Mapper.CreateMap<Applicant, ApplicantVm>();
            Mapper.CreateMap<ApplicantVm, Applicant>();
            Mapper.CreateMap<CoApplicant, LeadSheetVM>().ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoFirstName), opt => opt.MapFrom<string>(src => src.FirstName)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoMiddleName), opt => opt.MapFrom<string>(src => src.MiddleName)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoLastName), opt => opt.MapFrom<string>(src => src.LastName)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoDateOfBirth), opt => opt.MapFrom<DateTime?>(src => src.DateOfBirth)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoSocialSecurityNumber), opt => opt.MapFrom<string>(src => src.SocialSecurityNumber)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoYearsInSchool), opt => opt.MapFrom<int>(src => src.YearsInSchool)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoHomePhone), opt => opt.MapFrom<string>(src => src.HomePhone)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoCellPhone), opt => opt.MapFrom<string>(src => src.CellPhone)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoWorkPhone), opt => opt.MapFrom<string>(src => src.WorkPhone)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoFax), opt => opt.MapFrom<string>(src => src.Fax)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoEmailAddress), opt => opt.MapFrom<string>(src => src.EmailAddress));
            Mapper.CreateMap<LeadSheetVM, CoApplicant>().ForMember((Expression<Func<CoApplicant, object>>)(dest => dest.FirstName), opt => opt.MapFrom<string>(src => src.CoFirstName)).ForMember((Expression<Func<CoApplicant, object>>)(dest => dest.MiddleName), opt => opt.MapFrom<string>(src => src.CoMiddleName)).ForMember((Expression<Func<CoApplicant, object>>)(dest => dest.LastName), opt => opt.MapFrom<string>(src => src.CoLastName)).ForMember((Expression<Func<CoApplicant, object>>)(dest => dest.DateOfBirth), opt => opt.MapFrom<DateTime?>(src => src.CoDateOfBirth)).ForMember((Expression<Func<CoApplicant, object>>)(dest => dest.SocialSecurityNumber), opt => opt.MapFrom<string>(src => src.CoSocialSecurityNumber)).ForMember((Expression<Func<CoApplicant, object>>)(dest => dest.YearsInSchool), opt => opt.MapFrom<int>(src => src.CoYearsInSchool)).ForMember((Expression<Func<CoApplicant, object>>)(dest => dest.HomePhone), opt => opt.MapFrom<string>(src => src.CoHomePhone)).ForMember((Expression<Func<CoApplicant, object>>)(dest => dest.Fax), opt => opt.MapFrom<string>(src => src.CoFax)).ForMember((Expression<Func<CoApplicant, object>>)(dest => dest.EmailAddress), opt => opt.MapFrom<string>(src => src.CoEmailAddress));
            Mapper.CreateMap<Property, LeadSheetVM>().ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.PropAddress), opt => opt.MapFrom<string>(src => src.Address)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.PropCity), opt => opt.MapFrom<string>(src => src.City)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.propCounty), opt => opt.MapFrom<string>(src => src.County)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.PropZipCode), opt => opt.MapFrom<string>(src => src.ZipCode)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.PropState), opt => opt.MapFrom<UsStateEnum>(src => src.State));
            Mapper.CreateMap<LeadSheetVM, Property>().ForMember((Expression<Func<Property, object>>)(dest => dest.City), opt => opt.MapFrom<string>(src => src.PropCity)).ForMember((Expression<Func<Property, object>>)(dest => dest.State), opt => opt.MapFrom<string>(src => src.PropState)).ForMember((Expression<Func<Property, object>>)(dest => dest.County), opt => opt.MapFrom<string>(src => src.propCounty)).ForMember((Expression<Func<Property, object>>)(dest => dest.Address), opt => opt.MapFrom<string>(src => src.PropAddress)).ForMember((Expression<Func<Property, object>>)(dest => dest.ZipCode), opt => opt.MapFrom<string>(src => src.PropZipCode));
            Mapper.CreateMap<Credit, LeadSheetVM>();
            Mapper.CreateMap<LeadSheetVM, Credit>();
            Mapper.CreateMap<Mortgage, LeadSheetVM>().ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.LeanPosition), opt => opt.MapFrom<int?>(src => src.Position));
            Mapper.CreateMap<LeadSheetVM, Mortgage>().ForMember((Expression<Func<Mortgage, object>>)(dest => dest.Position), opt => opt.MapFrom<int?>(src => src.LeanPosition));
            Mapper.CreateMap<Employment, LeadSheetVM>();
            Mapper.CreateMap<LeadSheetVM, Employment>();
            Mapper.CreateMap<Employment, EmploymentVM>();
            Mapper.CreateMap<EmploymentVM, Employment>();
            Mapper.CreateMap<EmploymentVM, LeadSheetVM>().ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoEmployerName), opt => opt.MapFrom<string>(src => src.EmployerName)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoPosition), opt => opt.MapFrom<string>(src => src.Position)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoScheduleType), opt => opt.MapFrom<string>(src => src.ScheduleType)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoHireDate), opt => opt.MapFrom<DateTime?>(src => src.HireDate)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoEndDate), opt => opt.MapFrom<DateTime?>(src => src.EndDate)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoPayPeriod), opt => opt.MapFrom<PayScheduleEnum?>(src => src.PayPeriod)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoEmployerName), opt => opt.MapFrom<string>(src => src.EmployerName)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoEarningsBeforeTax), opt => opt.MapFrom<decimal?>(src => src.EarningsBeforeTax)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoEmploymentType), opt => opt.MapFrom<string>(src => src.EmploymentType)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoLastYear_SE_EarningsReported_IRS), opt => opt.MapFrom<decimal?>(src => src.LastYear_SE_EarningsReported_IRS)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoLastYearDepreciationAsReported), opt => opt.MapFrom<decimal?>(src => src.LastYearDepreciationAsReported)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoYear2_SE_EarningsReported_IRS), opt => opt.MapFrom<decimal?>(src => src.Year2_SE_EarningsReported_IRS)).ForMember((Expression<Func<LeadSheetVM, object>>)(dest => dest.CoYear2DepreciationAsReported), opt => opt.MapFrom<decimal?>(src => src.Year2DepreciationAsReported));
            Mapper.CreateMap<LeadSheetVM, EmploymentVM>().ForMember((Expression<Func<EmploymentVM, object>>)(dest => dest.EmployerName), opt => opt.MapFrom<string>(src => src.CoEmployerName)).ForMember((Expression<Func<EmploymentVM, object>>)(dest => dest.Position), opt => opt.MapFrom<string>(src => src.CoPosition)).ForMember((Expression<Func<EmploymentVM, object>>)(dest => dest.ScheduleType), opt => opt.MapFrom<string>(src => src.CoScheduleType)).ForMember((Expression<Func<EmploymentVM, object>>)(dest => dest.HireDate), opt => opt.MapFrom<DateTime?>(src => src.CoHireDate)).ForMember((Expression<Func<EmploymentVM, object>>)(dest => dest.EndDate), opt => opt.MapFrom<DateTime?>(src => src.CoEndDate)).ForMember((Expression<Func<EmploymentVM, object>>)(dest => dest.PayPeriod), opt => opt.MapFrom<PayScheduleEnum?>(src => src.CoPayPeriod)).ForMember((Expression<Func<EmploymentVM, object>>)(dest => dest.EmployerName), opt => opt.MapFrom<string>(src => src.CoEmployerName)).ForMember((Expression<Func<EmploymentVM, object>>)(dest => dest.EarningsBeforeTax), opt => opt.MapFrom<decimal?>(src => src.CoEarningsBeforeTax)).ForMember((Expression<Func<EmploymentVM, object>>)(dest => dest.EmploymentType), opt => opt.MapFrom<string>(src => src.CoEmploymentType)).ForMember((Expression<Func<EmploymentVM, object>>)(dest => dest.LastYear_SE_EarningsReported_IRS), opt => opt.MapFrom<decimal?>(src => src.CoLastYear_SE_EarningsReported_IRS)).ForMember((Expression<Func<EmploymentVM, object>>)(dest => dest.LastYearDepreciationAsReported), opt => opt.MapFrom<decimal?>(src => src.CoLastYearDepreciationAsReported)).ForMember((Expression<Func<EmploymentVM, object>>)(dest => dest.Year2_SE_EarningsReported_IRS), opt => opt.MapFrom<decimal?>(src => src.CoYear2_SE_EarningsReported_IRS)).ForMember((Expression<Func<EmploymentVM, object>>)(dest => dest.Year2DepreciationAsReported), opt => opt.MapFrom<decimal?>(src => src.CoYear2DepreciationAsReported));
        }
    }
}

