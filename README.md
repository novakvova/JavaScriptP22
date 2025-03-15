sudo systemctl enable goose.itstep.click

sudo systemctl start goose.itstep.click

sudo systemctl status goose.itstep.click

sudo systemctl stop goose.itstep.click

sudo systemctl restart goose.itstep.click


``` /etc/nginx/sites-available/
server {
    server_name   goose.itstep.click *.goose.itstep.click;
    location / {
       proxy_pass         http://localhost:2208;
       proxy_http_version 1.1;
       proxy_set_header   Upgrade $http_upgrade;
       proxy_set_header   Connection keep-alive;
       proxy_set_header   Host $host;
       proxy_cache_bypass $http_upgrade;
       proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
       proxy_set_header   X-Forwarded-Proto $scheme;
    }
}
```

sudo systemctl restart nginx

certbot