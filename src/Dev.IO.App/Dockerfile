FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

WORKDIR /src

COPY ["src/Dev.IO.App/Dev.IO.App.csproj", "Dev.IO.App/"]
COPY ["src/DevIO.Business/DevIO.Business.csproj", "DevIO.Business/"]
COPY ["src/DevIO.Data/DevIO.Data.csproj", "DevIO.Data/"]

COPY ["src/Dev.IO.App/entrypoint.sh", "Dev.IO.App/entrypoint.sh"]
RUN chmod +x ./Dev.IO.App/entrypoint.sh
COPY . .

RUN dotnet restore Dev.IO.App/Dev.IO.App.csproj

WORKDIR "src/Dev.IO.App"

RUN dotnet build Dev.IO.App.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Dev.IO.App.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dev.IO.App.dll"]

CMD /bin/bash ./Dev.IO.App/entrypoint.sh