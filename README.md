Implemented Api using .net7 for Conway's Game of Life. Conway's Game of Life - Wikipedia (https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life)

The API has 4 basic endpoints:

	1. Allows uploading a new board state, returns id of board
		a. POST /boards
		b. body should be bidimensional array. 
			example: [ [true,false], [true,false] ]

	2. Get next state for board, returns next state
		a. GET /boards/{id}/nextstate
		b. id is the id of the board

	3. Gets x number of states away for board
		a. GET /boards/{id}/state/{iterations}
		b. id is the id of the board
		c. iterations is the number of states away

	4. Gets final state for board. If board doesn't go to conclusion after x number of attempts, returns error
		a. GET /boards/{id}/state/final
		b. id is the id of the board


You will need a MongoDb instance running to use the API. You can use the following docker command to run a MongoDb instance:

	docker run --name mongodb -p 27017:27017 -e MONGO_INITDB_ROOT_USERNAME=netdocuments -e MONGO_INITDB_ROOT_PASSWORD=netdocuments mongo

For configuration is based the follow appsettings:
```json
{
	"MongoDb": {
		"ConnectionString": "mongodb://netdocuments:netdocuments@localhost:27017/admin",
		"Database": "GameOfLife",
		"BoardCollection": "Boards"
	},
	"App": {
		"MaxNumberOfAttempts": 100
	},
	"BoardCache": {
		"ExpirationInMs": 60000
	}
}
```

For integration tests, is being used Mongo2Go

The choice of using mongoDb is because the board can be pretty large, and a nonSql database is more suitable for this kind of data.

The API is using a memory cache to store the boards, so it doesn't need to query the database every time. 
The cache expiration can be configured "BoardCache:ExpirationInMs". (integer miliseconds)

In order to have a better performance and avoid inserting data that is the same,
the boardId is calculated based on the array of the board.

Once the bord is calculate and have a final state, we update the data to mongodb, in order to avoid big recalculation


Enjoy.