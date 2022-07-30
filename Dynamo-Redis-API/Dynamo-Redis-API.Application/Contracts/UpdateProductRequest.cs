﻿namespace Dynamo_Redis_API.Application.Contracts
{
    public class UpdateProductRequest
    {
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public decimal Price { get; set; }

        public string Currency { get; set; } = default!;
    }
}
