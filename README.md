# hacktoberfest_leaderboard
A simple leaderboard for the GitHub Hacktoberfest

## Getting started
To get the project running locally, all you need is an IDE,
[docker](https://www.docker.com/) and 
[docker-compose](https://docs.docker.com/compose/). Once you have installed 
these, simply run the following command:

```.sh
docker-compose -f docker-compose.dev.yml up
```

This will launch two docker containers, one with the backend and one with the
website. Both are hot-reload enabled, which means you can now start hacking, 
modify files etc. and on a save, the corresponding container will reload.

The website can be then be accessed via `http://localhost:8080` and the API
is available under `http://localhost:5000` and `https://localhost:5001`. Happy
hacking!

**Note:** Sometimes the Backend may fail with some kind of "polling watcher"
exception. This can happend due to how the different volumes are mounted in
docker. Simply quit and run docker compose again, and the problem should 
disappear.