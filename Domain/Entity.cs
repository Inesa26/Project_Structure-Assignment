﻿namespace Domain
{
    public abstract class Entity
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
