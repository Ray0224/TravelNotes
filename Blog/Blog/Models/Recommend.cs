﻿using System;
using System.Collections.Generic;

namespace Blog.Models;

public partial class Recommend
{
    public int LabelId { get; set; }

    public int? UserId { get; set; }

    public string? Gender { get; set; }

    public string? Weather { get; set; }

    public string? Interest { get; set; }

    public string? Interest2 { get; set; }

    public string? Interest3 { get; set; }

    public string? Location { get; set; }
}
