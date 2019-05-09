FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build

WORKDIR /app
ARG project
COPY ${project}/. .
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime

WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "${project}.dll"]