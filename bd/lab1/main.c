#include <stdio.h>
#include <stdlib.h>

#define NAME_LEN 80
#define RACE_NAME_LEN 100

typedef struct {
    int id;
    int year;
    int first_race;
    int races_cnt;
    char race_director[NAME_LEN];
    char tyre_supplier[NAME_LEN];
} t_season;

typedef struct {
    int id;
    int season_id;
    int lap_count;
    int lap_length;
    int next_race;
    int empty;
    int offset;
    char name[RACE_NAME_LEN];
} t_race;

typedef struct {
    int n;
    t_race* array;
} array_pair;

FILE *fseasons_ind;
FILE *fseasons;
FILE *fraces;

t_season* get_m(int id) {
    fseek(fseasons_ind, sizeof(int)*(id-1), SEEK_SET); //todo check edge case
    int offset;
    fread(&offset, sizeof(int), 1, fseasons_ind);
    t_season* season = (t_season*)malloc(sizeof(t_season));
    if (offset < 0) {
        return NULL;
    }
    fseek(fseasons, offset, SEEK_SET);
    fread(season, sizeof(t_season), 1, fseasons);
    return season;
}

t_season* get_m_ui() {
    int id;
    printf("enter season id: ");
    scanf("%d", &id);
    t_season* season = get_m(id);
    if (season == NULL) {
        printf("this season doesn`t exist.\n");
    } else {
        printf("-----------\n");
        printf("season id: %d\nyear: %d\nrace director: %s\ntyre supplier: %s\n", 
        season->id, season->year, season->race_director, season->tyre_supplier);
    }
    return season;
}

array_pair get_s(t_season* season, int race_id) {
    array_pair result;
    int n = season->races_cnt;
    if (n == 0) {
        result.n = 0;
        result.array = NULL;
        return result;
    }
    t_race* races = (t_race*)malloc(n*sizeof(t_race));
    result.n = n;
    result.array = races;
    if (race_id != 0) {
        result.n = -1;
    }
    int next_race = season->first_race;
    int i = 0;
    while (next_race != -1) {
        fseek(fraces, next_race, SEEK_SET);
        t_race tmp;
        fread(&tmp, sizeof(t_race), 1, fraces);
        next_race = tmp.next_race;
        if (!tmp.empty) {
            races[i] = tmp;
            if (race_id == tmp.id) {
                result.n = i;
            }
            i++;
        }
    }
    return result;
} 

t_race* get_s_ui(int all_races) {
    t_season *season = get_m_ui();
    if (season == NULL) {
        return NULL;
    }
    int race_id;
    if (all_races) {
        race_id = 0;
    } else {
        printf("enter race id: ");
        scanf("%d", &race_id);
    }

    array_pair result = get_s(season, race_id);

    if (result.n == -1) {
        printf("this race doesn`t exist.\n");
        return NULL;
    }
    if (!all_races) {
        int ind = result.n;
        printf("-----------\n");
        printf("race id: %d\nname: %s\nlap count: %d\nlap length: %d\n", 
        result.array[ind].id, result.array[ind].name, result.array[ind].lap_count, result.array[ind].lap_length);
        return &result.array[ind];
    }

    if (result.n == 0) {
        printf("this season doesn`t have races.\n");
        return NULL;
    }
    for (int i = 0; i < result.n; i++) {
        printf("-----------\n");
        printf("race id: %d\nname: %s\nlap count: %d\nlap length: %d\n", 
        result.array[i].id, result.array[i].name, result.array[i].lap_count, result.array[i].lap_length);
    }
    return NULL;
}

void update_m(t_season* season) {
    fseek(fseasons_ind, sizeof(int)*(season->id-1), SEEK_SET);
    int offset;
    fread(&offset, sizeof(int), 1, fseasons_ind);
    fseek(fseasons, offset, SEEK_SET);
    fwrite(season, sizeof(t_season), 1, fseasons);
}

void update_m_ui() {
    t_season *season = get_m_ui();
    if (season == NULL) {
        return;
    }
    printf("choose field to change (1: year, 2: race director, 3: tyre supplier)\n");
    int command;
    scanf("%d", &command);
    switch (command)
    {
    case 1:
        printf("enter year: ");
        scanf("%d", &(season->year));
        break;
    case 2:
        printf("enter race director: ");
        scanf("%s", season->race_director);
        break;
    case 3:
        printf("enter tyre supplier: ");
        scanf("%s", season->tyre_supplier);
        break;
    default:
        printf("invalid command.");
        return;
    }
    update_m(season);
    printf("done.\n");
}

void update_s_ui() {
    t_race *race = get_s_ui(0);
    if (race == NULL) {
        return;
    }
    printf("choose field to change (1: name, 2: lap count, 3: lap length)\n");
    int command;
    scanf("%d", &command);
    switch (command)
    {
    case 1:
        printf("enter name: ");
        scanf("%s", race->name);
        break;
    case 2:
        printf("enter lap count: ");
        scanf("%d", &(race->lap_count));
        break;
    case 3:
        printf("enter lap length: ");
        scanf("%d", &(race->lap_length));
        break;
    default:
        printf("invalid command.");
        return;
    }
    fseek(fraces, race->offset, SEEK_SET);
    fwrite(race, sizeof(t_race), 1, fraces);
    printf("done.\n");
}

void insert_m_ui() {
    t_season season;
    printf("enter season year, race director and tyre supplier:\n");
    scanf("%d %s %s", &season.year, season.race_director, season.tyre_supplier);
    fseek(fseasons_ind, 0L, SEEK_END);
    season.id = ftell(fseasons_ind) / sizeof(int) + 1;
    season.races_cnt = 0;
    season.first_race = -1;
    fseek(fseasons, 0L, SEEK_END);
    int offset = ftell(fseasons);
    fwrite(&season, sizeof(t_season), 1, fseasons);
    fwrite(&offset, sizeof(int), 1, fseasons_ind);
    printf("season added with id %d.\n", season.id);
}

void insert_s_ui() {
    int season_id;
    printf("enter season id: ");
    scanf("%d", &season_id);
    t_season* season = get_m(season_id);
    if (season == NULL) {
        printf("this season doesn`t exist.");
        return;
    }
    season->races_cnt++;
    t_race race;
    race.season_id = season_id;
    if (season->first_race == -1) {
        race.id = 1;
    } else {
        int id;
        fseek(fraces, season->first_race, SEEK_SET);
        fread(&id, sizeof(int), 1, fraces);
        race.id = id + 1;
    }
    race.next_race = season->first_race;
    race.empty = 0;
    printf("enter race name, lap count and lap length:\n");
    scanf("%s %d %d", race.name, &race.lap_count, &race.lap_length);
    fseek(fraces, 0L, SEEK_END);
    season->first_race = ftell(fraces);
    race.offset = season->first_race;
    fwrite(&race, sizeof(t_race), 1, fraces);
    update_m(season);
    printf("race added with id %d.\n", race.id);
}

void del_s(t_race *race) {
    race->empty = 1;
    fseek(fraces, race->offset, SEEK_SET);
    fwrite(race, sizeof(t_race), 1, fraces);
    t_season *season = get_m(race->season_id);
    season->races_cnt--;
    update_m(season);
}

void del_m_ui() {
    t_season *season = get_m_ui();
    if (season == NULL) {
        return;
    }
    array_pair races = get_s(season, 0);
    for (int i = 0; i < races.n; i++) {
        del_s(&races.array[i]);
    }
    fseek(fseasons_ind, sizeof(int)*(season->id-1), SEEK_SET);
    int tmp = -1;
    fwrite(&tmp, sizeof(int), 1, fseasons_ind);
}


void del_s_ui() {
    t_race *race = get_s_ui(0);
    if (race == NULL) {
        return;
    }
    del_s(race);
}

int main() {
    fraces = fopen("races.fl", "w+");
    fseasons = fopen("seasons.fl", "w+");
    fseasons_ind = fopen("seasons.ind", "w+");

    while (1) {
        int command;
        printf("-----------\n");
        printf("choose command:\n0: exit\n1: get-m\n2: get-s\n3: del-m\n4: del-s\n5: update-m\n6: update-s\n7: insert-m\n8: insert-s\n");
        scanf("%d", &command);
        switch (command) {
            case 0:
                return 0;
            case 1:
                get_m_ui();
                break;
            case 2:
                printf("print concret race(0) or all races(1)?");
                int all_races;
                scanf("%d", &all_races);
                get_s_ui(all_races);
                break;
            case 3:
                del_m_ui();
                break;
            case 4:
                del_s_ui();
                break;
            case 5:
                update_m_ui();
                break;
            case 6:
                update_s_ui();
                break;
            case 7:
                insert_m_ui();
                break;
            case 8:
                insert_s_ui();
                break;
            default:
                printf("invalid command.");
                break;
        }
    }
    return 0;
}