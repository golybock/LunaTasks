import ICardView from "../card/view/ICardView";

export function toDictionary(array: ICardView[]){
    let cards = new Map<string, ICardView[]>;

    array.forEach(item => {
        const status = JSON.stringify(item.status);

        let val = cards.get(status);

        if(!val){
            cards.set(status, [item]);
        }else{
            cards.set(status, [...val, item])
        }
    })

    return Array.from(cards, ([status, card]) => ({status, card}));
}

export function toUsersDictionary(array: ICardView[]){
    let cards = new Map<string, ICardView[]>;

    array.forEach(item => {

        if(item.users.length == 0){
            return;
        }

        const user = JSON.stringify(item.users[0]);

        let val = cards.get(user);

        if(!val){
            cards.set(user, [item]);
        }else{
            cards.set(user, [...val, item])
        }
    })

    return Array.from(cards, ([user, card]) => ({user, card}));
}