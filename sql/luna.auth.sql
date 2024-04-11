create table user_auth
(
    id       uuid  not null
        primary key,
    user_id  uuid  not null,
    password bytea not null
);

