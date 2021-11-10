# command sample from root
# docker build -f ./devops/Dockerfile -t api-demo --build-arg port=8700 --build-arg name=ObservabilityDemo.Api ../src

# Args to replace with --build-arg
ARG port=8700
ARG name="ObservabilityDemo.Api"

# Args
ARG dist=bin
ARG protocol=http
ARG csproj=${name}/${name}.csproj
ARG file=${name}.dll

### BUILD APP #####################################################
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
ARG dist
ARG name
ARG csproj

WORKDIR /src
COPY ./ ./

RUN ls
RUN echo "${csproj}"
RUN dotnet restore "${csproj}"
RUN dotnet publish -c Release -o "${dist}" "${csproj}"

### BUILD IMAGE ####################################################
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ARG dist
ARG protocol
ARG port
ARG name
ARG file

# Default Environment
ENV ASPNETCORE_ENVIRONMENT="Development"

# NewRelic
RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
    && echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
    && wget https://download.newrelic.com/548C16BF.gpg \
    && apt-key add 548C16BF.gpg \
    && apt-get update \
    && apt-get install -y newrelic-netcore20-agent
ENV CORECLR_ENABLE_PROFILING=0 
ENV CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A}
ENV CORECLR_NEWRELIC_HOME=/usr/local/newrelic-netcore20-agent
ENV CORECLR_PROFILER_PATH=/usr/local/newrelic-netcore20-agent/libNewRelicProfiler.so
ENV NEW_RELIC_LICENSE_KEY=<inject-into-this-container>
ENV NEW_RELIC_DISTRIBUTED_TRACING_ENABLED=true
ENV NEW_RELIC_APP_NAME="${name} ${ASPNETCORE_ENVIRONMENT}"

# Copy files to /app
COPY --from=build-env /src/${dist} /app
RUN ls
 
# Expose port for the Web API traffic
ENV ASPNETCORE_URLS ${protocol}://+:${port}
EXPOSE ${port}

# Run application
WORKDIR /app
RUN ls
ENV file=${file}
ENTRYPOINT dotnet ${file}