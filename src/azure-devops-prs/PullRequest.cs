using System;

namespace AzureDevOpsPrs
{
    public class PullRequest
    {
        public int Id { get; }
        public string Title { get; }
        public string Description { get; }
        public string CreatedBy { get; }
        public DateTime CreatedAt { get; }
        public Uri Url { get; }
        public string Repository { get; }
        public string Status { get; }

        private PullRequest(Builder builder)
        {
            Id = builder.Id;
            Title = builder.Title;
            Description = builder.Description;
            CreatedBy = builder.CreatedBy;
            CreatedAt = builder.CreatedAt;
            Url = builder.Url;
            Repository = builder.Repository;
            Status = builder.Status;
        }

        public class Builder
        {
            internal int Id { get; private set; }
            internal string Title { get; private set; }
            internal string Description { get; private set; }
            internal string CreatedBy { get; private set; }
            internal DateTime CreatedAt { get; private set; }
            internal Uri Url { get; private set; }
            internal string Repository { get; private set; }
            internal string Status { get; private set; }

            public Builder SetId(int id)
            {
                Id = id;
                return this;
            }

            public Builder SetTitle(string title)
            {
                Title = title;
                return this;
            }

            public Builder SetDescription(string description)
            {
                Description = description;
                return this;
            }

            public Builder SetCreatedBy(string createdBy)
            {
                CreatedBy = createdBy;
                return this;
            }

            public Builder SetCreatedAt(DateTime createdAt)
            {
                CreatedAt = createdAt;
                return this;
            }

            public Builder SetUrl(Uri url)
            {
                Url = url;
                return this;
            }

            public Builder SetRepository(string repository)
            {
                Repository = repository;
                return this;
            }

            public Builder SetStatus(string status)
            {
                Status = Status;
                return this;
            }

            public PullRequest Build()
            {
                return new PullRequest(this);
            }
        }
    }

}