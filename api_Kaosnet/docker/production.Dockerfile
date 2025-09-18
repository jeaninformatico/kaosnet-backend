FROM nginx:1.27.4

LABEL maintainer="Elsiosanches@gmail.com; EdwinBetanc0urt@outlook.com;" \
	description="Web Jean Informatico"


# Default expose port
EXPOSE 80


# Init ENV with default values
ENV PUBLIC_PATH="/" \
	TZ="America/Caracas"


# Add operative system dependencies
RUN echo "Set Timezone..." && \
	ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && \
	echo $TZ > /etc/timezone && \
	apt-get update && \
	apt-get install -y --no-install-recommends tzdata && \
	apt-get clean && \
	rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/*


# Copy src files
COPY --chown=nginx:nginx dist/ /usr/share/nginx/html/

# Verify files were copied correctly
RUN [ -f "/usr/share/nginx/html/index.html" ] || { echo "Error: index.html missing!"; exit 1; }


# Security config to no root user
RUN chmod -R 755 /usr/share/nginx/html && \
	chown -R nginx:nginx /var/cache/nginx && \
	chown -R nginx:nginx /var/log/nginx && \
	touch /var/run/nginx.pid && \
	chown nginx:nginx /var/run/nginx.pid

USER nginx


# Start nginx
CMD ["nginx", "-g", "daemon off;"]
