﻿namespace VideoCall.Core.Account.Requests;

public class RegisterRequest
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}
