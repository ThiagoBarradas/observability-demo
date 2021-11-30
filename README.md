# Observability Demo

This is a demo project with NewRelic

## Running project

From root path, set `NR_LICENSE` with your license key as environment variable and use docker-compose to up services

```
export NR_LICENSE=<your-new-relic-license-key>
docker-compose -f devops/docker-compose.yml up --force-recreate --build
```
