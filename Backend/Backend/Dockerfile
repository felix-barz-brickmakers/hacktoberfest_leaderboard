FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
COPY . /build 
WORKDIR /build
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY --from=build /build/out /opt/leaderboard
WORKDIR /opt/leaderboard
EXPOSE 80
ENTRYPOINT [ "/opt/leaderboard/Backend" ] 
