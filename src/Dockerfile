FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build

ARG project
WORKDIR /app
COPY ${project}/. .
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime

ARG project
ENV project=${project}
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT dotnet ${project}.dll