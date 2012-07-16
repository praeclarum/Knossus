
!requite accounts
!require reviews
!require languages

.movie {
	title: string,
	languages: [language],
	reviews: [review],
	
	average_rating: select avg(rating) from reviews,
}

# select m.title, avg(rating), min(rating), max(rating)
# from movie_review join movie m on m.id = movie_id
# group by movie_id;


/movies_and_ratings
GET
	movies = select {
		title: m.title,
		avg_rating: avg(rating),
		min_rating: min(rating),
	} from movie_review join movie m on m.id = movie_id group by movie_id;

	return movies;



/movies_and_languages
GET
	movies = select {
		title: m.title,
		languages: select {
			name: ml.name,
		} from movie_language ml join language l on l == ml.language where ml.movie == m,
	} from movie m;

	return movies;
	
/director/{name:\w+}
	director = select id from directors where name == $name limit 1;
	
	ratings = select {
		average: avg(rating),
		stddev: stddev(rating),
	} from movies where director == $director;
	
	movies = select {
		title: m.title,
	} from movies m where director == $director;

GET
	return ratings, movies;

/{id:\d+}

GET

PUT
	in string name;
	

