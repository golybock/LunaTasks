create table files
(
    id                  uuid                                   not null
        primary key,
    path                text                                   not null,
    file_type           integer                                not null,
    workspace_id        uuid                                   not null,
    uploaded_by_user_id uuid                                   not null,
    uploaded_date       timestamp with time zone default now() not null,
    deleted             boolean                  default false not null
);

