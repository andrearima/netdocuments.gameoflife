Once you finish, submit results to a git repo on Github or some other online git service and share a link to that repo.

Here are some tips on what to implement on your test to guarantee your success: 
- Use of clean code
- Use of unit tests
- Use of good comments
- Use of diferent layers to build the solution
- Use of strategies to improve performance
- Use of code conventions, code syntax and apply best practices 
- Use of solid principles
- Good error handling
- Use of modern stacks
- Do not reinvent the wheel. If there is something that already exists, use it. 
- Document your code and be organized

 
All the instructions for completing it are in the document, but feel free to reach out if you have questions.
--------------------------------------------------------------------------------------------------------------------------


The following should be done in C# using net7.0 as the target framework.

Implement an API for Conway's Game of Life. Conway's Game of Life - Wikipedia (https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life)

The API should have implementations for at least the following:
	1. Allows uploading a new board state, returns id of board
	2. Get next state for board, returns next state
	3. Gets x number of states away for board
	4. Gets final state for board. If board doesn't go to conclusion after x number of attempts, returns error

The service you write should be able to restart/crash/etc... but retain the state of the boards.

The code you write should be production ready. You don’t need to implement any authentication/authorization. 
Be prepared to show how your code meets production ready requirements.

This may take up to 4 – 5 hours to complete. Come prepared to talk about your architecture and coding decisions.


Domain Rules
Every cell interacts with its eight neighbors, which are the cells that are horizontally, vertically, or diagonally adjacent. At each step in time, the following transitions occur:

1. Any live cell with fewer than two live neighbours dies, as if by underpopulation.
2. Any live cell with two or three live neighbours lives on to the next generation.
3. Any live cell with more than three live neighbours dies, as if by overpopulation.
4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.


docker run --name mongodb -p 27017:27017 -e MONGO_INITDB_ROOT_USERNAME=netdocuments -e MONGO_INITDB_ROOT_PASSWORD=netdocuments mongo

mongodb://netdocuments:netdocuments@localhost:27017/admin


[ 
	[true,false], 
	[true,false] 
]

[ 
	[false,false], 
	[false,false] 
]

1E48C4420B7073BC11916C6C1DE226BB