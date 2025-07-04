
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app


RUN apt-get update && apt-get install -y openssl


RUN mkdir /https && \
    openssl req -x509 -nodes -days 365 \
      -newkey rsa:2048 \
      -keyout /https/aspnetcore.key \
      -out /https/aspnetcore.crt \
      -subj "/CN=localhost" && \
    openssl pkcs12 -export \
      -out /https/aspnetcore.pfx \
      -inkey /https/aspnetcore.key \
      -in /https/aspnetcore.crt \
      -passout pass:password


EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src


COPY ["WebApplication1.csproj", "./"]
RUN dotnet restore "./WebApplication1.csproj"


COPY . .
RUN dotnet build "WebApplication1.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
RUN dotnet publish "WebApplication1.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app


COPY --from=publish /app/publish .


ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS="http://+:5001;https://+:5002"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/https/aspnetcore.pfx"
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="password"


ENTRYPOINT ["dotnet", "WebApplication1.dll"]
