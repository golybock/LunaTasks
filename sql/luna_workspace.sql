create table workspace
(
    id                uuid                                   not null
        primary key,
    name              varchar(150)                           not null,
    created_timestamp timestamp with time zone default now() not null,
    created_user_id   uuid                                   not null
);

create table workspace_users
(
    id           serial
        primary key,
    user_id      uuid not null
        constraint workspace_users_pk
            unique,
    workspace_id uuid not null
        constraint workspace_users___fk
            references workspace
);

