﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPIAssessment.Dto;

public partial class EditField
{
    [JsonIgnore]
    public Guid Id { get; set; }
    [JsonIgnore]
    public Guid? FormId { get; set; }
    [JsonIgnore]
    public Guid? ColumnId { get; set; }
    [JsonIgnore]
    public Guid? DomainTableId { get; set; }
    [JsonIgnore]
    public Guid? ViewResourceId { get; set; }
    [JsonIgnore]
    public Guid? ModifyResourceId { get; set; }
    [JsonIgnore]
    public string? AddChangeDeleteFlag { get; set; }

    public int? Sequence { get; set; }

    public string? Type { get; set; }

    public int? TextAreaRows { get; set; }

    public int? TextAreaCols { get; set; }

    public string? Label { get; set; }

    public string? DisplayColumns { get; set; }

    public int? QuoteReadOnly { get; set; }
    [JsonIgnore]
    public int? QuoteRequired { get; set; }

    public int? QuoteDisplay { get; set; }

    public int? QuoteDisabled { get; set; }

    public int? PolicyReadOnly { get; set; }

    public int? PolicyRequired { get; set; }

    public int? PolicyDisplay { get; set; }

    public int? PolicyDisabled { get; set; }

    public string? RequiredCondition { get; set; }

    public int? AmendablePostIssuance { get; set; }

    public int? AmendablePreRenewal { get; set; }

    public string? Default { get; set; }

    public string? Minimum { get; set; }

    public string? Maximum { get; set; }

    public string? Mask { get; set; }

    public string? Help { get; set; }

    public string? HelpText { get; set; }

    public int? DisplayController { get; set; }

    public string? Condition { get; set; }

    public string? Comment { get; set; }
    [JsonIgnore]
    public string? DialogFileType { get; set; }

    public string? DialogFileName { get; set; }

    public int? Auditable { get; set; }

    public string? AuditCondition { get; set; }

    public string? XslValue { get; set; }

    public Guid? RefTableId { get; set; }

    public int? TextDisplaySize { get; set; }

    public string? LinkText { get; set; }

    public int? AuditViewOnly { get; set; }

}