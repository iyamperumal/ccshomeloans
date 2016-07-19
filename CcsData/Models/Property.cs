namespace CcsData.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class Property
    {
        [MaxLength(100)]
        public virtual string Address { get; set; }

        [MaxLength(50)]
        public virtual string AlternateApn { get; set; }

        public virtual decimal? AnnualHomeonersInsuranceAmount { get; set; }

        [ForeignKey("Applicant_ID")]
        public CcsData.Models.Applicant Applicant { get; set; }

        public int Applicant_ID { get; set; }

        public virtual int? AssessmentYear { get; set; }

        [Display(Name="Number Of Bathrooms"), DefaultValue(1)]
        public virtual float? Bathrooms { get; set; }

        [Display(Name="Number Of Bedrooms")]
        public virtual int? Bedrooms { get; set; }

        [MaxLength(50)]
        public virtual string BldgLivingArea { get; set; }

        [MaxLength(20)]
        public virtual string CarrierRoute { get; set; }

        [MaxLength(50)]
        public virtual string CensusTract { get; set; }

        [MaxLength(50)]
        public virtual string City { get; set; }

        [MaxLength(50), Display(Name="Condo Association Name")]
        public virtual string CondoAssociationName { get; set; }

        [Display(Name="Confirmed Value")]
        public virtual decimal? ConfirmedMarketValue { get; set; }

        [DataType(DataType.Date), Display(Name="Confirmed Value Date")]
        public virtual DateTime? ConfirmedMarketValueDate { get; set; }

        public virtual ContructionTypeEnum? ConstructionType { get; set; }

        [MaxLength(50)]
        public virtual string County { get; set; }

        [MaxLength(50)]
        public virtual string CountyLandUseCode { get; set; }

        [Display(Name="Any Mainteneance Needed"), MaxLength(50)]
        public virtual string DefferedMaintenance { get; set; }

        public virtual float? EquityPercent { get; set; }

        [Display(Name="Estimated LTV")]
        public virtual decimal? EstimatedLTV { get; set; }

        [Display(Name="Estimated LTV Date")]
        public virtual DateTime? EstimatedLTVdate { get; set; }

        [Display(Name="Estimated Value")]
        public virtual decimal? EstimatedMarketValue { get; set; }

        [Display(Name="Estimated Value Date"), DataType(DataType.Date)]
        public virtual DateTime? EstimatedMarketValueDate { get; set; }

        public virtual string Garage { get; set; }

        public virtual bool? HasGarage { get; set; }

        [Display(Name="Condition Of Home compare to Neighbors")]
        public virtual string HomeCondition { get; set; }

        [MaxLength(50)]
        public string HomesteadExemption { get; set; }

        [MaxLength(20)]
        public virtual string HouseNumber { get; set; }

        [MaxLength(20)]
        public string HouseNumber2 { get; set; }

        [MaxLength(20)]
        public virtual string HouseNumberPrefix { get; set; }

        [MaxLength(20)]
        public virtual string HouseNumberSuffix { get; set; }

        [MaxLength(50)]
        public virtual string ImprovedPecent { get; set; }

        public virtual decimal? ImprovedValue { get; set; }

        public virtual bool? IsMobileHome { get; set; }

        public virtual decimal? LandValue { get; set; }

        [Display(Name="Location"), MaxLength(50)]
        public virtual string LocationType { get; set; }

        [Display(Name="Acreage")]
        public virtual float? LotAcreage { get; set; }

        public virtual float? LotArea { get; set; }

        [Display(Name="Mobile Home Model"), MaxLength(50)]
        public virtual string MobileHomeModel { get; set; }

        public virtual YesNoAns? OwnerOccupied { get; set; }

        [Display(Name="This Property is my"), UIHint("EnumCheck")]
        public OwnershipTypeEnum? OwnerShipType { get; set; }

        [MaxLength(50)]
        public virtual string ParcelId { get; set; }

        [MaxLength(20)]
        public virtual string PostDirection { get; set; }

        [MaxLength(20)]
        public virtual string PreDirection { get; set; }

        [Key]
        public int Property_Id { get; set; }

        [Display(Name="Improvements")]
        public virtual string PropertyImprovements { get; set; }

        [MaxLength(50)]
        public virtual string PropertyType { get; set; }

        [Display(Name="Property Type is"), UIHint("EnumCheck")]
        public PropertyTypeEnum? PropertyTypeApp { get; set; }

        [Display(Name="Purshase Date"), DataType(DataType.Date)]
        public virtual DateTime? PurshaseDate { get; set; }

        [Display(Name="Purshase Price")]
        public virtual decimal? PurshasePrice { get; set; }

        [DefaultValue(1)]
        public YesNoAns Rural { get; set; }

        [Display(Name="Total Square footage")]
        public virtual double? SqFt { get; set; }

        public UsStateEnum State { get; set; }

        [MaxLength(20)]
        public virtual string StateLandUseCode { get; set; }

        [MaxLength(50)]
        public virtual string StreetName { get; set; }

        [MaxLength(50)]
        public virtual string StreetNameSuffix { get; set; }

        [MaxLength(20)]
        public virtual string SwimmingPoolPresent { get; set; }

        [MaxLength(50)]
        public virtual string TaxReduction { get; set; }

        [MaxLength(50)]
        public virtual string TitleHeld { get; set; }

        public virtual decimal? TotalAssessedValue { get; set; }

        [MaxLength(20)]
        public virtual string UnitNumber { get; set; }

        public virtual int? YearBuilt { get; set; }

        public virtual decimal? yearlyTaxAmount { get; set; }

        [MaxLength(10)]
        public virtual string Zip4 { get; set; }

        [MaxLength(20)]
        public virtual string ZipCode { get; set; }

        [MaxLength(20)]
        public virtual string ZipPlusZip4 { get; set; }
    }
}

