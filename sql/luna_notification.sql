create database luna_notification;

create table notification
(
    id           uuid                                   not null
        primary key,
    user_id      uuid                                   not null,
    text         text                                   not null,
    created      timestamp with time zone default now() not null,
    created_user uuid                                   not null,
    priority     integer                  default 1     not null,
    image_url    text,
    read         boolean                  default false not null,
    read_at      timestamp with time zone
);

