create table role
(
    id   serial       not null
        primary key,
    name varchar(150) not null
        unique
);

create table status
(
    id           uuid                  not null
        primary key,
    name         varchar(150)          not null,
    hex_color    varchar(7)            not null,
    workspace_id uuid                  not null,
    deleted      boolean default false not null
);

create table tag
(
    id           uuid                  not null
        primary key,
    name         varchar(150)          not null,
    hex_color    varchar(7)            not null,
    workspace_id uuid                  not null,
    deleted      boolean default false not null
);

create table page
(
    id                uuid                                   not null
        primary key,
    name              varchar(150)                           not null,
    description       varchar(500),
    header_image      text,
    created_timestamp timestamp with time zone default now() not null,
    created_user_id   uuid                                   not null,
    workspace_id      uuid                                   not null
);

create table type
(
    id           uuid                  not null
        primary key,
    name         varchar(150)          not null,
    hex_color    varchar(7)            not null,
    workspace_id uuid                  not null,
    deleted      boolean default false not null
);

create table card
(
    id                uuid                                   not null
        primary key,
    header            varchar(300)                           not null,
    content           text,
    description       text,
    card_type_id      uuid                                   not null
        references type,
    page_id           uuid                                   not null
        references page,
    created_user_id   uuid                                   not null,
    created_timestamp timestamp with time zone default now() not null,
    deadline          timestamp with time zone,
    previous_card_id  uuid
        references card,
    deleted           boolean                  default false not null
);

create table block_card
(
    id                    serial
        primary key,
    card_id               uuid                                   not null
        references card,
    comment               text,
    blocked_user_id       uuid                                   not null,
    start_block_timestamp timestamp with time zone default now() not null,
    end_block_timestamp   timestamp with time zone
);

create table card_tags
(
    id      serial
        primary key,
    card_id uuid                  not null
        references card,
    tag_id  uuid                  not null
        references tag,
    deleted boolean default false not null
);

create table card_users
(
    id      serial
        primary key,
    card_id uuid                  not null
        references card,
    user_id uuid                  not null,
    deleted boolean default false not null
);

create table comment
(
    id             serial                not null
        primary key,
    card_id        uuid                  not null
        references card,
    user_id        uuid                  not null,
    comment        text,
    attachment_url text,
    deleted        boolean default false not null
);

create table card_status
(
    id            serial
        primary key,
    card_id       uuid                    not null
        references card,
    status_id     uuid                    not null
        references status,
    set_timestamp timestamp default now() not null
);

