create table Brand (
	id serial primary key,
	name text not null,
	is_active bool not null default false
);

create table Model (
	id serial primary key,
	brand_id int not null,
	name text not null,
	is_active bool not null default false,
	
	foreign key (brand_id) references Brand(id)
);
